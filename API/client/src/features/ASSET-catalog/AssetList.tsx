import { Grid, List } from "@mui/material";
import { Asset } from "../../app/models/asset";
import AssetCard from "./AssetCard";

interface Props {
  assets: Asset[];
}

export default function AssetList({ assets }: Props) {
  return (
    <Grid container spacing={4}>
      {assets.map((asset) => (
        <Grid item xs={4}>
          <AssetCard key={asset.id} asset={asset} />
        </Grid>
      ))}
    </Grid>
  );
}
