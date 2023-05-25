import { Grid } from "@mui/material";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import PRCard from "./PRCard";

interface Props {
  purchaserequisitions: PurchaseRequisition[];
}

export default function PRList({ purchaserequisitions }: Props) {
  return (
    <Grid container spacing={3}>
      {purchaserequisitions.map((purchaserequisition) => (
        <Grid item xs={4} key={purchaserequisition.id}>
          <PRCard purchaserequisition={purchaserequisition} />
        </Grid>
      ))}
    </Grid>
  );
}
