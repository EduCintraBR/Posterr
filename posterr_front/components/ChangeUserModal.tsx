import React, { useState, useEffect } from "react";
import toast from "react-hot-toast";
import { useUser } from "../context/userContext";

const ChangeUserModal: React.FC<{ isOpen: boolean; onClose: () => void }> = ({
  isOpen,
  onClose,
}) => {
  const { users, setUser } = useUser();
  const [selectedUserId, setSelectedUserId] = useState<string>("");

  useEffect(() => {
    const storedUserId = localStorage.getItem("selectedUserId");

    if (storedUserId) {
      setSelectedUserId(storedUserId);
    } else if (users && users.length > 0) {
      setSelectedUserId(users[0].identifier);
    }
  }, [users]);

  const handleUserChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedUserId(e.target.value);
  };

  const handleSave = () => {
    if (!users || users.length === 0) {
      toast.error("No users available");
      return;
    }

    const selectedUser = users.find(
      (user) => user.identifier === selectedUserId
    );
    if (selectedUser) {
      setUser(selectedUser.identifier);
      onClose();
    } else {
      toast.error("Please select a user");
    }
  };

  if (!isOpen) return null;

  if (!users) {
    return (
      <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
        <div className="bg-white p-4 rounded shadow-lg w-1/3">
          <p>Loading users...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
      <div className="bg-white p-4 rounded shadow-lg w-1/3">
        <h2 className="text-xl mb-4">Change User</h2>
        <select
          value={selectedUserId}
          onChange={handleUserChange}
          className="w-full p-2 border rounded"
        >
          <option
            value=""
            disabled
          >
            Select a user
          </option>
          {users.map((user) => (
            <option
              key={user.identifier}
              value={user.identifier}
            >
              {user.username}
            </option>
          ))}
        </select>
        <div className="flex justify-end mt-4">
          <button
            onClick={onClose}
            className="p-2 border rounded mr-2"
          >
            Cancel
          </button>
          <button
            onClick={handleSave}
            className="p-2 border rounded bg-blue-500 text-white"
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};

export default ChangeUserModal;
