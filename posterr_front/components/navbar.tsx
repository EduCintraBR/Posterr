import React, { useState } from "react";
import ChangeUserModal from "../components/ChangeUserModal";
import { useUser } from "../context/userContext";

const Navbar: React.FC = () => {
  const { user, setUser, users } = useUser();
  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  const handleChangeUser = (userId: string) => {
    setUser(userId);
    setIsModalOpen(false);
  };

  return (
    <nav className="bg-gray-800 p-4">
      <div className="container mx-auto flex justify-between items-center">
        <h1 className="text-white text-2xl">Posterr</h1>
        <div className="flex items-center">
          <button
            onClick={() => handleOpenModal()}
            className="text-white bg-blue-500 p-2 rounded"
          >
            Change User
          </button>
        </div>
        <ChangeUserModal
          isOpen={isModalOpen}
          onClose={handleCloseModal}
        />
      </div>
    </nav>
  );
};

export default Navbar;