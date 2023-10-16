export interface User {
    UserName: string;
    email: string;
    token: string;
    roles?: string[]; // Include roles as an array of strings
}
