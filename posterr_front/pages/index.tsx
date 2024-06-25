import React, { useState, useEffect, useCallback, useRef } from "react";
import PostList from "../components/postList";
import PostForm from "../components/postForm";
import { useUser } from "../context/userContext";
import toast from "react-hot-toast";
import { useDebounce } from "use-debounce";
import LoadingIndicator from "@/components/loading";
import { createPost, repost } from "@/utils/postUtils"
import ContentPost from "@/models/content";

const Home: React.FC = () => {
  const [posts, setPosts] = useState<ContentPost[]>([]);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [loading, setLoading] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [sortOption, setSortOption] = useState("latest");
  const { user } = useUser();

  const [debouncedSearchTerm] = useDebounce(searchTerm, 500);

  const observer = useRef<IntersectionObserver | null>(null);

  const apiUrl = useCallback(
    (pageNumber: number) => {
      let limit = pageNumber === 1 ? 15 : 20;
      let base = "http://localhost:8082/api/v1/content";
      let url = `${base}/latest?offset=${(pageNumber - 1) * 20}&limit=${limit}`;

      if (sortOption === "trending") {
        url = `${base}/trending?offset=${(pageNumber - 1) * 20}&limit=${limit}`;
      }

      if (debouncedSearchTerm) {
        url = `${base}/search?offset=${
          (pageNumber - 1) * 20
        }&limit=${limit}&keyword=${debouncedSearchTerm}`;
      }

      return url;
    },
    [sortOption, debouncedSearchTerm]
  );

  const fetchPosts = useCallback(async (url: string, pageNumber: number) => {
    setLoading(true);
    try {
      const res = await fetch(url);
      if (!res.ok) {
        throw new Error("Failed to fetch posts");
      }
      const data: ContentPost[] = await res.json();
      if (pageNumber === 1) {
        setPosts(data);
      } else {
        setPosts((prevPosts) => [...prevPosts, ...data]);
      }
      setHasMore(data.length >= (pageNumber === 1 ? 15 : 20));
    } catch (error: any) {
      toast.error(error.message);
    } finally {
      setLoading(false);
    }
  }, []);

  const loadPosts = useCallback(
    async (pageNumber: number) => {
      const url = apiUrl(pageNumber);
      await fetchPosts(url, pageNumber);
    },
    [apiUrl, fetchPosts]
  );

  useEffect(() => {
    loadPosts(1);
  }, [loadPosts]);

  useEffect(() => {
    if (page > 1) {
      loadPosts(page);
    }
  }, [page, loadPosts]);

  const handleCreatePost = async (content: string) => {
    const newPost = {
      userId: user?.identifier || "",
      content,
      originalPostId: null,
    };
    createPost(newPost, loadPosts, setSearchTerm); // Chama a função utilitária
  };

  const handleRepost = async (postId: string) => {
    repost(postId, posts, user, loadPosts, setSearchTerm); // Chama a função utilitária
  };

  const lastPostRef = useCallback(
    (node: HTMLDivElement | null) => {
      if (loading || !hasMore) return;
      if (observer.current) observer.current.disconnect();

      observer.current = new IntersectionObserver((entries) => {
        if (entries[0].isIntersecting && hasMore) {
          setPage((prevPage) => prevPage + 1);
        }
      });

      if (node) observer.current.observe(node);
    },
    [loading, hasMore]
  );

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(e.target.value);
    setPage(1); // Reset page to 1 when search term changes
  };

  const handleSortChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setSortOption(e.target.value);
    setPage(1); // Reset page to 1 when sort option changes
  };

  return (
    <div className="container mx-auto p-4">
      <h2 className="text-2xl mb-4">What's happening?</h2>
      <div className="flex justify-between mb-4">
        <input
          type="text"
          value={searchTerm}
          onChange={handleSearchChange}
          placeholder="Search posts"
          className="p-2 border rounded w-1/2"
        />
        <select
          value={sortOption}
          onChange={handleSortChange}
          className="p-2 border rounded"
        >
          <option value="latest">Latest</option>
          <option value="trending">Trending</option>
        </select>
      </div>
      <PostForm onCreatePost={handleCreatePost} />
      <PostList
        posts={posts}
        onRepost={handleRepost}
        lastPostRef={lastPostRef}
      />
      {loading && <LoadingIndicator />}
    </div>
  );
};

export default Home;