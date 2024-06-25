import User from "@/models/user";
import React, {
  createContext,
  useState,
  ReactNode,
  useContext,
  useEffect,
} from "react";

interface UserContextType {
  user: User | null;
  users: User[];
  setUser: (userId: string) => void;
}

const UserContext = createContext<UserContextType | undefined>(undefined);

export const UserProvider: React.FC<{ children: ReactNode }> = ({
  children,
}) => {
  const [user, setUserState] = useState<User | null>(null);
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const res = await fetch("http://localhost:8082/api/v1/users");
        if (!res.ok) {
          throw new Error("Failed to fetch users");
        }
        const data: User[] = await res.json();
        setUsers(data);

        const storedUserId = localStorage.getItem("selectedUserId");
        console.log("storedUserId", storedUserId);
        console.log("formattedUsers", data);

        if (storedUserId) {
          const selectedUser = data.find(
            (user) => user.identifier === storedUserId
          );
          if (selectedUser) {
            setUserState(selectedUser);
          }
        } else if (data.length > 0) {
          setUserState(data[0]);
        }
      } catch (error) {
        console.error(error);
      }
    };

    fetchUsers();
  }, []);

  const setUser = (userId: string) => {
    const selectedUser = users.find((user) => user.identifier === userId);
    if (selectedUser) {
      setUserState(selectedUser);
      localStorage.setItem("selectedUserId", userId);
    }
  };

  return (
    <UserContext.Provider value={{ user, users, setUser }}>
      {children}
    </UserContext.Provider>
  );
};

export const useUser = () => {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error("useUser must be used within a UserProvider");
  }
  return context;
};