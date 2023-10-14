export interface User {
    email: string;
    token: string;
    roles?: string[]; // Include roles as an array of strings
}
