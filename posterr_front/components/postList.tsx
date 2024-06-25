import React, { useEffect, useRef } from "react";
import Post from "./post";
import ContentPost from "@/models/content";

interface PostListProps {
  posts: ContentPost[];
  onRepost: (postId: string) => void;
  lastPostRef: (node: HTMLDivElement | null) => void;
}

const PostList: React.FC<PostListProps> = ({
  posts,
  onRepost,
  lastPostRef,
}) => {
  const observer = useRef<IntersectionObserver | null>(null);

  useEffect(() => {
    const options = {
      root: null,
      rootMargin: "0px",
      threshold: 1.0,
    };

    const handleObserver: IntersectionObserverCallback = (entries) => {
      if (entries[0].isIntersecting) {
        console.log("Intersection detected!");
      }
    };

    observer.current = new IntersectionObserver(handleObserver, options);

    return () => {
      if (observer.current) {
        observer.current.disconnect();
      }
    };
  }, [lastPostRef]);

  return (
    <div>
      {posts.map((post, index) => (
        <div key={post.identifier}>
          <Post
            post={post}
            onRepost={onRepost}
          />
          {index === posts.length - 1 && (
            <div ref={lastPostRef}>
              {/* This div is used to detect the last post */}
            </div>
          )}
        </div>
      ))}
    </div>
  );
};

export default PostList;
