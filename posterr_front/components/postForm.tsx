import { useState } from 'react';

interface PostFormProps {
  onCreatePost: (content: string) => void;
}

const PostForm: React.FC<PostFormProps> = ({ onCreatePost }) => {
  const [content, setContent] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (content.length === 0) return;
    onCreatePost(content);
    setContent('');
  };

  return (
    <form onSubmit={handleSubmit} className="mb-4">
      <textarea
        className="w-full p-2 border rounded"
        value={content}
        onChange={(e) => setContent(e.target.value)}
        placeholder="What's happening?"
        maxLength={777}
      />
      <button type="submit" className="mt-2 p-2 bg-blue-500 text-white rounded">
        Post
      </button>
    </form>
  );
};

export default PostForm;