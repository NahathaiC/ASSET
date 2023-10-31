import { useState } from "react";
import { useUser,} from "../../app/hooks/useUsers";
import { UserDto } from "../../app/models/user";
import { useAppDispatch } from "../../app/store/configureStore";
import UserForm from "./UserMN";
import { Edit, Delete } from "@mui/icons-material";
import {
  Box,
  Typography,
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Button,
} from "@mui/material";
import AppPagination from "../../app/components/AppPagination";
import { setPageNumber } from "../catalog/catalogSlice";

//ตัวแทน Inventory ของคลิป หรือ PRstatus

//UserMN.tsx
export default function UserUpdate() {
  const { getusers, metaData } = useUser();
  const dispatch = useAppDispatch();
  const [editMode, setEditMode] = useState(false);
  const [selectedUser, setSelectedUser] = useState<UserDto>();

  function handleSelectUser(user: UserDto) {
    setSelectedUser(user);
    setEditMode(true);
  }

  function cancelEdit() {
    if (selectedUser) setSelectedUser(undefined);
    setEditMode(false);
  }

  if (editMode) return <UserForm user={selectedUser} cancelEdit={cancelEdit} />;
  return (
    <>
      <Box display="flex" justifyContent="space-between">
        <Typography sx={{ p: 2 }} variant="h4">
          จัดการข้อมูลพื้นฐานบัญชีผู้ใช้
        </Typography>
      </Box>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>รหัสพนักงาน</TableCell>
              <TableCell align="center">ชื่อบัญชีผู้ใช้</TableCell>
              <TableCell align="center">อีเมลล์</TableCell>
              <TableCell align="center">ตำแหน่ง</TableCell>
              <TableCell align="center">สาขา</TableCell>
              <TableCell align="center">แผนก</TableCell>
              <TableCell align="center">เบอร์โทร</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {getusers.map((user) => (
              <TableRow
                key={user.id}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {user.id}
                </TableCell>
                <TableCell align="center">{user.userName}</TableCell>
                <TableCell align="center">{user.email}</TableCell>
                <TableCell align="center">{user.position}</TableCell>
                <TableCell align="center">{user.department}</TableCell>
                <TableCell align="center">{user.section}</TableCell>
                <TableCell align="center">{user.phone}</TableCell>
                <TableCell align="right">
                  <Button
                    onClick={() => handleSelectUser(user)}
                    startIcon={<Edit />}
                  />
                  <Button startIcon={<Delete />} color="error" />
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      {metaData && (
        <Box sx={{ pt: 2 }}>
          <AppPagination
            metaData={metaData}
            onPageChange={(page: number) =>
              dispatch(setPageNumber({ pageNumber: page }))
            }
          />
        </Box>
      )}
    </>
  );
}
