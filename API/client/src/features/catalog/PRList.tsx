import { Grid } from "@mui/material";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import PRCard from "./PRCard";
import { useAppSelector } from "../../app/store/configureStore";
import PRCardSkeleton from "./PRCardSkeleton";

interface Props {
  purchaserequisitions: PurchaseRequisition[];
}

export default function PRList({ purchaserequisitions }: Props) {
  const { prsLoaded } = useAppSelector((state) => state.catalog);
  return (
    <Grid container spacing={3}>
      {purchaserequisitions.map((purchaserequisition) => (
        <Grid item xs={4} key={purchaserequisition.id}>
          {!prsLoaded ? (
            <PRCardSkeleton />
          ) : (
            <PRCard purchaserequisition={purchaserequisition} />
          )}
        </Grid>
      ))}
    </Grid>
  );
}
