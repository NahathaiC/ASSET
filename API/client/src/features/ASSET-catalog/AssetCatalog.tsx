import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import {
  setPageNumber,
} from "./AssetSlice";
import AssetList from "./AssetList";
import useAssets from "../../app/hooks/useAssets";
import { Grid, Paper } from "@mui/material";
import AppPagination from "../../app/components/AppPagination";
import AssetSearch from "./AssetSearch";

const sortOptions = [
  {value: 'name', label: 'Alphabetical'},
];

export default function AssetCatalog() {
  const { assets, metaData } = useAssets();
  const { status, assetParams } = useAppSelector((state) => state.assetcatalog);
  const dispatch = useAppDispatch();

  if (status.includes("pending") || !metaData)
    return <LoadingComponent message="Loading..." />;
  return (
    <Grid container spacing={4}>
      <Grid item xs={3}>
        <Paper sx={{ mb: 2 }}>
          <AssetSearch />
        </Paper>
      </Grid>
      <Grid item xs={9}>
        <AssetList assets={assets} />
      </Grid>
      <Grid item xs={3} />
      <Grid item xs={9} sx={{ mb: 2 }}>
        <AppPagination
          metaData={metaData}
          onPageChange={(page: number) =>
            dispatch(setPageNumber({ pageNumber: page }))
          }
        />
      </Grid>
    </Grid>
  );
}
