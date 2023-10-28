import {
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
import { useEffect, useRef } from "react";
import { useParams } from "react-router-dom";
import { useReactToPrint } from "react-to-print";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { assetSelectors, fetchAssetAsync } from "./AssetSlice";
import LoadingComponent from "../../app/layout/LoadingComponent";

export default function AssetDetails() {
  const generatePDF = useReactToPrint({
    content: () => componentPDF.current,
    documentTitle: "AssetDetails",
  });
  const componentPDF = useRef(null);
  const dispatch = useAppDispatch();

  const { id } = useParams<{ id: string }>();
  const asset = useAppSelector((state) =>
    assetSelectors.selectById(state, id!)
  );
  const {status} = useAppSelector(state => state.assetcatalog)

  useEffect(() => {
    if (!asset && id) dispatch(fetchAssetAsync(id));
  }, [id, dispatch, asset]);

  if (status.includes('pending')) return <LoadingComponent message="Loading..."/>

  if (!asset) return <h3> ไม่พบทรัพย์สินที่ตามหา </h3>;

  return (
    <>
      <div ref={componentPDF} style={{ width: "100%" }}>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <Typography variant="subtitle1">
              Asset Id: {asset.id} No. {asset.no}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Typography variant="h4" color="primary">
              {asset.serailNo}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Typography variant="h4" color="primary">
              {asset.name}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Divider />
            <br />
            <img
              src={asset.assetPic}
              alt={asset.name}
              style={{
                width: "300px",
                height: "200px",
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
                    <TableCell>Type:</TableCell>
                    <TableCell>{asset.type}</TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell>Model:</TableCell>
                    <TableCell>{asset.model}</TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell>Manufacturer:</TableCell>
                    <TableCell>{asset.manufacturer}</TableCell>
                  </TableRow>

                  <TableRow>
                    <TableCell>Status:</TableCell>
                    <TableCell>{asset.assetStatus}</TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell>Person in charge:</TableCell>
                    <TableCell>{asset.personInCharge.userName}</TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell>Owner:</TableCell>
                    <TableCell>{asset.owner.ownerDesc}</TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell>Stock at:</TableCell>
                    <TableCell>{asset.stock.type}</TableCell>
                  </TableRow>
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>
        </Grid>
      </div>
      <Grid item xs={12}>
        <Button variant="contained" color="primary" onClick={generatePDF}>
          ดาวน์โหลดเอกสาร .pdf
        </Button>
      </Grid>
    </>
  );
}
