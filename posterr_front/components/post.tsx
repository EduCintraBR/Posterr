import { useState } from "react";
import Modal from "./modal";
import { format } from "date-fns";
import ContentPost from "@/models/content";

interface PostProps {
  post: ContentPost;
  onRepost: (postId: string) => void;
}

const Post: React.FC<PostProps> = ({ post, onRepost }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleRepostConfirm = () => {
    onRepost(post.contentPostID);
    setIsModalOpen(false);
  };

  let formattedDate = "";
  try {
    const dateObject = new Date(post.date);
    if (!isNaN(dateObject.getTime())) {
      formattedDate = format(dateObject, "MMMM d, yyyy h:mm a");
    } else {
      throw new Error("Invalid date format");
    }
  } catch (error: any) {
    console.error("Error parsing date:", error.message);
    formattedDate = "Invalid Date";
  }

  const isOwnPost = post.user.identifier === localStorage.getItem("userId");
  const isRepost = post.action === 2;

  return (
    <div className="border p-4 mb-4 rounded shadow">
      <div className="flex justify-between">
        <p className="text-gray-700 py-2 text-lg">
          <strong>@{post.user.username}</strong>
        </p>
        {isRepost && <p className="py-4 text-gray-500 text-sm">Reposted</p>}
      </div>

      <p className="py-4 px-2 text-gray-900 break-words overflow-hidden">
        {post.contentPost.content}
      </p>

      <div className="flex justify-between">
        {!isOwnPost && !isRepost && (
          <button
            onClick={() => setIsModalOpen(true)}
            className="mt-4 p-2 bg-gray-300 rounded"
          >
            Repost
          </button>
        )}
        <p></p>
        <p className="mt-6 p-2 text-gray-400 text-sm">
          Posted at {formattedDate}
        </p>
      </div>

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onConfirm={handleRepostConfirm}
        title="Confirm Repost"
      >
        <p>Are you sure you want to repost this?</p>
      </Modal>
    </div>
  );
};

export default Post;
