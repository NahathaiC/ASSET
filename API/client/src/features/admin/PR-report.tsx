import React, { useState } from "react";
import {
  Box,
  Typography,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Snackbar,
  TableContainer,
  Button,
} from "@mui/material";
import { useAppDispatch } from "../../app/store/configureStore";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import usePRs from "../../app/hooks/usePRs";
import { setPageNumber } from "../catalog/catalogSlice";
import PRForm from "./PRForm";
import agent from "../../app/api/agent";
import AppPagination from "../../app/components/AppPagination";
import { Link } from "react-router-dom";
import FindInPageIcon from '@mui/icons-material/FindInPage';


export default function PRreport() {
  const { purchaserequisitions, metaData } = usePRs();
  const dispatch = useAppDispatch();
  const [editMode, setEditMode] = useState(false);
  const [selectedPR, setSelectedPR] = useState<PurchaseRequisition | undefined>(
    undefined
  );
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");

  const handleStartDateChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setStartDate(event.target.value);
  };

  const handleEndDateChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setEndDate(event.target.value);
  };

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

  const getStatusBackgroundColor = (status: string) => {
    switch (status) {
      case "Approved":
        return "#66bb6a"; // Green
      case "Pending":
        return "#ffb74d"; // No background color
      case "Disapproved":
        return "#d32f2f"; // Red
      default:
        return ""; // Default background color
    }
  };

  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState("");

  const showSnackbar = (message: any) => {
    setSnackbarMessage(message);
    setSnackbarOpen(true);
  };

  const filterByDate = (startDate: string, endDate: string) => {
    if (startDate && endDate) {
      const endDateNextDay = new Date(endDate);
      endDateNextDay.setDate(endDateNextDay.getDate() + 1); // Add one day to the end date
      return purchaserequisitions.filter((purchaseRequisition) => {
        const purchaseDate = new Date(purchaseRequisition.createDate);
        return (
          purchaseDate >= new Date(startDate) && purchaseDate < endDateNextDay
        );
      });
    }
    return purchaserequisitions;
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
          รายงานการขอซื้อ
        </Typography>
      </Box>

      <label>จากวันที่  </label>
      <input type="date" value={startDate} onChange={handleStartDateChange} />

      <label>  จนถึงวันที่  </label>
      <input type="date" value={endDate} onChange={handleEndDateChange} />

      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>รหัส</TableCell>
              <TableCell align="center">รายการขอซื้อ</TableCell>
              <TableCell align="center">ขอซื้อโดย</TableCell>
              <TableCell align="center">วันที่ขอซื้อ</TableCell>
              <TableCell align="center">สาขา</TableCell>
              <TableCell align="center">แผนก</TableCell>
              <TableCell align="center">จำนวน</TableCell>
              <TableCell align="center">ราคาต่อหน่วย</TableCell>
              <TableCell align="center">สถานะการขอซื้อ</TableCell>
              <TableCell align="center">รายละเอียด</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {filterByDate(startDate, endDate).map((purchaseRequisition) => {
              // Get the background color based on the status
              const backgroundColor = getStatusBackgroundColor(
                purchaseRequisition.status
              );

              const formattedDate = purchaseRequisition.createDate.slice(0, 10);

              return (
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
                    {purchaseRequisition.requestUser}
                  </TableCell>
                  <TableCell align="center">{formattedDate}</TableCell>
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
                    {purchaseRequisition.unitPrice} THB
                  </TableCell>
                  <TableCell
                    align="center"
                    style={{ color: "white", backgroundColor }}
                  >
                    {purchaseRequisition.status}
                  </TableCell>
                  <TableCell align="center">
                    <Button
                      component={Link}
                      to={`/pr-catalog/${purchaseRequisition.id}`} // Update the path as needed
                      size="small"
                    >
                      <FindInPageIcon/>
                    </Button>
                  </TableCell>
                </TableRow>
              );
            })}
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
      <Snackbar
        anchorOrigin={{ vertical: "top", horizontal: "right" }}
        open={snackbarOpen}
        autoHideDuration={3000}
        onClose={() => setSnackbarOpen(false)}
        message={snackbarMessage}
      />
    </>
  );
}
