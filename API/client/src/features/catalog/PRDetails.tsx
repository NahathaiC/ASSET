import { Box, Button, Divider, Grid, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import agent from "../../app/api/agent";

export default function PRDetails() {
  const { id } = useParams<{ id: string }>();
  const [purchaseRequisition, setPRs] = useState<PurchaseRequisition | null>(null);
  const [loading, setLoading] = useState(true);
  const [showQuotation, setShowQuotation] = useState(false);

  useEffect(() => {
    id && agent.Catalog.details(parseInt(id))
        .then(response => setPRs(response))
        .catch(error => console.log(error))
        .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <h3>Loading...</h3>;

  if (!purchaseRequisition) return <h3>Purchase Requisition not found</h3>;

  const formattedDate = purchaseRequisition.createDate.slice(0, 10);
  const formatteduseDate = purchaseRequisition.useDate.slice(0, 10);
  const formattedQuotDate = purchaseRequisition.quotation?.createDate?.slice(0, 10);

  const handleShowQuotation = () => {
    setShowQuotation(!showQuotation);
  };

  return (
    <Grid container spacing={6}>
      <Grid item xs={12} md={6}>
        <img
          src={purchaseRequisition.prPicture}
          alt={purchaseRequisition.title}
          style={{
            width: "100%",
            height: "auto",
            objectFit: "contain",
            borderRadius: "8px",
            boxShadow: "0 2px 4px rgba(0, 0, 0, 0.2)",
          }}
        />
        <Divider />
        <Box mt={2}>
          <Button variant="outlined" onClick={handleShowQuotation}>
            {showQuotation ? "ใบเสนอราคา" : "แสดงใบเสนอราคา"}
          </Button>
          {showQuotation && purchaseRequisition.quotation && (
            <TableContainer>
              <Table>
                <TableBody>
                  <TableRow>
                    <TableCell>Supplier:</TableCell>
                    <TableCell>
                      {purchaseRequisition.quotation.supplier}
                    </TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell>Total Price:</TableCell>
                    <TableCell>
                      {purchaseRequisition.quotation.totalPrice} THB
                    </TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell>Create Date:</TableCell>
                    <TableCell>{formattedQuotDate}</TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell>Remark:</TableCell>
                    <TableCell>
                      {purchaseRequisition.quotation.remark}
                    </TableCell>
                  </TableRow>
                </TableBody>
              </Table>
            </TableContainer>
          )}
        </Box>
      </Grid>
      <Grid item xs={12} md={6}>
        <Typography variant="subtitle1">
          Created By: {purchaseRequisition.requestUser} on {formattedDate}
        </Typography>
        <Typography variant="h4" color="primary">
          {purchaseRequisition.prodDesc}
        </Typography>
        <Box mt={2} mb={2}>
          <Divider />
        </Box>
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
    </Grid>
  );
}
