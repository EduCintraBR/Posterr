import User from "@/models/user";
import React, { useState, useEffect } from "react";
import toast from "react-hot-toast";

const UserList: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const res = await fetch("http://localhost:8082/api/v1/users");
        if (!res.ok) {
          throw new Error("Failed to fetch users");
        }
        const data: User[] = await res.json();
        setUsers(data);
      } catch (error: any) {
        toast.error(error.message);
      } finally {
        setLoading(false);
      }
    };

    fetchUsers();
  }, []);

  if (loading) return <div>Loading...</div>;

  return (
    <div>
      <h2 className="text-2xl mb-4">Users</h2>
      <ul>
        {users.map((user) => (
          <li key={user.identifier}>{user.username}</li>
        ))}
      </ul>
    </div>
  );
};

export default UserList;
