import Post from "./post";
import User from "./user";

export default interface ContentPost {
    identifier: string;
    userID: string; 
    user: User ;
    date: Date;
    action: number;
    contentPostID: string;
    contentPost: Post;
  }