import {
  Box,
  Button,
  Divider,
  Grid,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import agent from "../../app/api/agent";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";

export default function PRDetails() {
  const { id } = useParams<{ id: string }>();
  const [purchaseRequisition, setPRs] = useState<PurchaseRequisition | null>(
    null
  );
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    id &&
      agent.Catalog.details(parseInt(id))
        .then((response) => setPRs(response))
        .catch((error) => console.log(error))
        .finally(() => setLoading(false));
  }, [id]);

  const handlePrintPDF = () => {
    // setPdfModalOpen(true);
    window.print();
  };

  if (loading) return <LoadingComponent message="Loading..." />;

  if (!purchaseRequisition) return <NotFound />;

  const formattedDate = purchaseRequisition.createDate.slice(0, 10);
  const formatteduseDate = purchaseRequisition.useDate.slice(0, 10);

  return (
    // <Grid container spacing={6}>
    //   <Grid item xs={12} md={6}>
    //     <img
    //       src={purchaseRequisition.prPicture}
    //       alt={purchaseRequisition.title}
    //       style={{
    //         width: "100%",
    //         height: "auto",
    //         objectFit: "contain",
    //         borderRadius: "8px",
    //         boxShadow: "0 2px 4px rgba(0, 0, 0, 0.2)",
    //       }}
    //     />
    //     <Divider />

    //   </Grid>
    //   <Grid item xs={12} md={6}>
    //     <Typography variant="subtitle1">
    //       Created By: {purchaseRequisition.requestUser} on {formattedDate}
    //     </Typography>
    //     <Typography variant="h4" color="primary">
    //       {purchaseRequisition.prodDesc}
    //     </Typography>
    //     <Box mt={2} mb={2}>
    //       <Divider />
    //     </Box>
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <Typography variant="subtitle1">
          Created By: {purchaseRequisition.requestUser} on {formattedDate}
        </Typography>
      </Grid>
      <Grid item xs={12}>
        <Typography variant="h4" color="primary">
          {purchaseRequisition.prodDesc}
        </Typography>
      </Grid>
      <Grid item xs={12}>
        <Divider />
        <img
           src={purchaseRequisition.prPicture}
           alt={purchaseRequisition.title}
           style={{
            width: "300px", // Set your desired width here
            height: "200px", // Set your desired height here
            objectFit: "contain",
            borderRadius: "8px",
            boxShadow: "0 2px 4px rgba(0, 0, 0, 0.2)",
           }}
         />
      </Grid>
      <Grid item xs={12}>
        <TableContainer>
          <Table>
            <TableBody>
              <TableRow>
                <TableCell>Department:</TableCell>
                <TableCell>{purchaseRequisition.department}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Section:</TableCell>
                <TableCell>{purchaseRequisition.section}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Required Date:</TableCell>
                <TableCell>{formatteduseDate}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Model:</TableCell>
                <TableCell>{purchaseRequisition.model}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Quantity:</TableCell>
                <TableCell>{purchaseRequisition.quantity}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Unit Price:</TableCell>
                <TableCell>{purchaseRequisition.unitPrice} THB</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Status:</TableCell>
                <TableCell>{purchaseRequisition.status}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Approved By:</TableCell>
                <TableCell>
                  {purchaseRequisition.approverName1}
                  <br />
                  {purchaseRequisition.approverName2}
                </TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </TableContainer>
      </Grid>
      <Grid item xs={12}>
        <Button variant="contained" color="primary" onClick={handlePrintPDF}>
          Print Details
        </Button>
      </Grid>
    </Grid>
  );
}
