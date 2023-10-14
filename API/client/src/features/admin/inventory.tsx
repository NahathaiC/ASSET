import {
  Box,
  Typography,
  Button,
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
} from "@mui/material";
import { Edit as EditIcon } from "@mui/icons-material";
import { useState } from "react";
import AppPagination from "../../app/components/AppPagination";
import { useAppDispatch } from "../../app/store/configureStore";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import usePRs from "../../app/hooks/usePRs";
import { setPageNumber } from "../catalog/catalogSlice";
import PRForm from "./PRForm";
import agent from "../../app/api/agent";

export default function Inventory() {
  const { purchaserequisitions, metaData } = usePRs();
  const dispatch = useAppDispatch();
  const [editMode, setEditMode] = useState(false);
  const [selectedPR, setSelectedPR] = useState<PurchaseRequisition | undefined>(
    undefined
  );

  const updateStatus = async (id: number, status: string) => {
    try {
      const response = await agent.Admin.updatePRStatus(id, status);
      if (response) {
        // Handle the success case, e.g., update the local data or show a message
      }
    } catch (error) {
      // Handle the error, e.g., show an error message
      console.error("Error updating status:", error);
    }
  };

  function handleSelectedPR(purchaseRequisition: PurchaseRequisition) {
    if (purchaseRequisition.status === "Pending") {
      setSelectedPR(purchaseRequisition);
      resetForm(); // Reset the form fields if needed
      setEditMode(true);
    } else {
      // Handle other cases, e.g., display a message or take appropriate action.
    }
  }

  function cancelEdit() {
    if (selectedPR) setSelectedPR(undefined);
    resetForm(); // Reset the form fields if needed
    setEditMode(false);
  }

  function resetForm() {
    // Reset form fields if needed
  }

  if (editMode)
    return <PRForm purchaserequisition={selectedPR} cancelEdit={cancelEdit} />;

  return (
    <>
      <Box display="flex" justifyContent="space-between">
        <Typography sx={{ p: 2 }} variant="h4">
          จัดการสถานะการขอซื้อ
        </Typography>
      </Box>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>รหัส</TableCell>
              <TableCell align="left">purchaseRequisition</TableCell>
              <TableCell align="center">สาขา</TableCell>
              <TableCell align="center">แผนก</TableCell>
              <TableCell align="center">จำนวน</TableCell>
              <TableCell align="center">สถานะการขอซื้อ</TableCell>
              <TableCell align="center">อนุมัติการขอซื้อ</TableCell>
              <TableCell align="center">ไม่อนุมัติการขอซื้อ</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {purchaserequisitions.map((purchaseRequisition) => (
              <TableRow
                key={purchaseRequisition.id}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {purchaseRequisition.id}
                </TableCell>
                <TableCell align="left">
                  <Box display="flex" alignItems="center">
                    <img
                      src={purchaseRequisition.prPicture}
                      alt={purchaseRequisition.title}
                      style={{ height: 50, marginRight: 20 }}
                    />
                    <span>{purchaseRequisition.title}</span>
                  </Box>
                </TableCell>
                <TableCell align="center">
                  {purchaseRequisition.department}
                </TableCell>
                <TableCell align="center">
                  {purchaseRequisition.section}
                </TableCell>
                <TableCell align="center">
                  {purchaseRequisition.quantity}
                </TableCell>
                <TableCell align="center">
                  {purchaseRequisition.status}
                </TableCell>
                <TableCell align="center">
                  {purchaseRequisition.status === "Pending" && (
                    <Button
                      onClick={() =>
                        updateStatus(purchaseRequisition.id, "Approved")
                      }
                      style={{ color: "white", backgroundColor: "green" }}
                    >
                      Approved
                    </Button>
                  )}
                </TableCell>
                <TableCell align="center">
                  {purchaseRequisition.status === "Pending" && (
                    <Button
                      onClick={() =>
                        updateStatus(purchaseRequisition.id, "Disapproved")
                      }
                      style={{ color: "white", backgroundColor: "red" }}
                    >
                      Disapproved
                    </Button>
                  )}
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
