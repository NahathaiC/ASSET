export interface User {
  id: number;
  userName: string;
  email: string;
  position: string;
  department: string;
  section: string;
  phone: string;
  token: string;
  roles: string[]; // Include roles as an array of strings
}

export interface UserDto {
  id: number;
  userName: string;
  email: string;
  position: string;
  department: string;
  section: string;
  phone: string;
}

export interface UserParams {
  pageNumber: number;
  pageSize: number;
}
