import { Grid, List } from "@mui/material";
import { Asset } from "../../app/models/asset";
import AssetCard from "./AssetCard";
import { useAppSelector } from "../../app/store/configureStore";
import PRCardSkeleton from "../catalog/PRCardSkeleton";

interface Props {
  assets: Asset[];
}

export default function AssetList({ assets }: Props) {
  const {assetsLoaded} = useAppSelector((state) => state.assetcatalog);
  return (
    <Grid container spacing={4}>
      {assets.map((asset) => (
        <Grid item xs={4} key={asset.id}>
          {!assetsLoaded ? (
            <PRCardSkeleton />
          ) : (
            <AssetCard asset={ asset }/>
          )}
        </Grid>
      ))}
    </Grid>
  );
}
