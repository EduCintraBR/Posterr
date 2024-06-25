import ContentPost from "@/models/content";
import User from "@/models/user";
import toast from "react-hot-toast";

export const createPost = async (
  newPost: any,
  loadPosts: Function,
  setSearchTerm: Function
) => {
  try {
    const res = await fetch("http://localhost:8082/api/v1/content/post", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(newPost),
    });
    if (!res.ok) {
      const error = await res.json();
      throw new Error(error.message || "Failed to create post");
    }
    setSearchTerm("");
    loadPosts(1); // Load posts immediately after creating post
  } catch (error: any) {
    toast.error(error.message);
  }
};

export const repost = async (
  postId: string,
  posts: ContentPost[],
  user: User | null,
  loadPosts: Function,
  setSearchTerm: Function
) => {
  const postToRepost = posts.find((post) => post.contentPostID === postId);
  if (postToRepost) {
    const repostData = {
      userId: user?.identifier || "",
      postId: postToRepost.contentPostID,
    };
    try {
      const res = await fetch("http://localhost:8082/api/v1/content/repost", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(repostData),
      });
      if (!res.ok) {
        const error = await res.json();
        throw new Error(error.message || "Failed to repost");
      }
      setSearchTerm("");
      loadPosts(1); // Load posts immediately after reposting
    } catch (error: any) {
      toast.error(error.message);
    }
  }
};