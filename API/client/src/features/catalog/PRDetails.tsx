import { Box, Divider, Grid, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";

export default function PRDetails() {
    const { id } = useParams<{ id: string }>();
    const [purchaseRequisition, setPRs] = useState<PurchaseRequisition | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        axios.get(`http://localhost:5050/api/PR/${id}`)
            .then(response => setPRs(response.data))
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }, [id])

    if (loading) return <h3>Loading...</h3>

    if (!purchaseRequisition) return <h3>Purchase Requisition not found</h3>

    const formattedDate = purchaseRequisition.createDate.slice(0, 10);
    const formatteduseDate = purchaseRequisition.useDate.slice(0, 10);

    return (
        <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
                <Box mb={2}>
                    <Typography variant="subtitle1">
                        {"Created By: "}{purchaseRequisition.requestUser} {" on "} {formattedDate}
                    </Typography>
                    <Divider />
                </Box>
                <Typography variant="h6" color="primary">
                    {purchaseRequisition.prodDesc}
                </Typography>
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
                                    {purchaseRequisition.approverName1}<br />
                                    {purchaseRequisition.approverName2}
                                </TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
        </Grid>
    )
}
