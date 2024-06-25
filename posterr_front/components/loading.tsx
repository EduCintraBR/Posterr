import React from "react";

const LoadingIndicator: React.FC = () => (
  <div className="fixed top-0 left-0 z-50 w-full h-full flex items-center justify-center bg-gray-800 bg-opacity-75">
    <svg
      className="animate-spin h-12 w-12 text-white"
      viewBox="0 0 24 24"
    >
      <circle
        className="opacity-25"
        cx="12"
        cy="12"
        r="10"
        stroke="currentColor"
        strokeWidth="4"
      />
      <path
        className="opacity-75"
        fill="currentColor"
        d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291l-1.414 1.414A9.953 9.953 0 014 12H0c0 2.687 1.053 5.122 2.929 7.071L6 17.29z"
      />
    </svg>
  </div>
);

export default LoadingIndicator;