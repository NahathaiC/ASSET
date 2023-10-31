import { Grid } from "@mui/material";
import AppPagination from "../../app/components/AppPagination";
import { useUser} from "../../app/hooks/useUsers";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { setPageNumber } from "../catalog/catalogSlice";

export default function UserCatalog() {
  const { getusers, metaData } = useUser();
  const { status, userParams } = useAppSelector((state) => state.usercatalog);
  const dispatch = useAppDispatch();

  if (status.includes("pending") || !metaData)
    return <LoadingComponent message="Loading User Details..." />;

  return (
    <Grid container spacing={4}>
      {/* <Grid item xs={9}>
        <PRList purchaserequisitions={purchaserequisitions} />
      </Grid>
      <Grid item xs={3} /> */}
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
