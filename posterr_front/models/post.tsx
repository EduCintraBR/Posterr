import User from "./user";

export default interface Post {
    identifier: string;
    userID: string;
    user: User; 
    content: string;
    repostCount: number;
    createdAt: Date;
  }