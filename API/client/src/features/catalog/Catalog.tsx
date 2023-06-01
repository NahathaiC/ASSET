import LoadingComponent from "../../app/layout/LoadingComponent";
import PRList from "./PRList";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { useEffect } from "react";
import { fetchFilters, fetchPRsAsync, prSelectors, setPrParams } from "./catalogSlice";
import { Box, FormLabel, Grid, Pagination, Paper, Typography } from "@mui/material";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import CheckboxButtons from "../../app/components/CheckboxButtons";
import AppPagination from "../../app/components/AppPagination";

const sortOptions = [
  { value: "CreatedDate", label: "วันที่ขอซื้อ" },
  { value: "UsingDate", label: "วันที่ต้องการใช้" },
];

export default function Catalog() {
  const purchaserequisitions = useAppSelector(prSelectors.selectAll);
  const { prsLoaded, status, filtersLoaded, department, section, prParams, metaData } = useAppSelector(
    (state) => state.catalog
  );
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (!prsLoaded) dispatch(fetchPRsAsync());
  }, [prsLoaded, dispatch]);

  useEffect(() => {
    if (!filtersLoaded) dispatch(fetchFilters());
  }, [dispatch, filtersLoaded]);

  if (status.includes("pending") || !metaData)
    return <LoadingComponent message="Loading Purchase requisitions..." />;

  return (
    <Grid container spacing={4}>
      <Grid item xs={3}>
        <Paper sx={{ mb: 2, p: 2 }}>
          <RadioButtonGroup
            selectedValue={prParams.orderBy}
            options={sortOptions}
            onChange={(e) => dispatch(setPrParams({orderBy: e.target.value}))}
          />
        </Paper>
        <Paper sx={{ mb: 2, p: 2 }}>
        <FormLabel id="demo-radio-buttons-group-label">สาขา</FormLabel>
          <CheckboxButtons 
            items={department}
            checked={prParams.department}
            onChange={(items: string[]) => dispatch(setPrParams({department: items}))}
          />
        </Paper>
        <Paper sx={{ mb: 2, p: 2 }}>
        <FormLabel id="demo-radio-buttons-group-label">แผนก</FormLabel>
        <CheckboxButtons 
            items={section}
            checked={prParams.section}
            onChange={(items: string[]) => dispatch(setPrParams({section: items}))}
          />
        </Paper>
      </Grid>
      <Grid item xs={9}>
        <PRList purchaserequisitions={purchaserequisitions} />
      </Grid>
      <Grid item xs={3} />
      <Grid item xs={9}>
        <AppPagination 
          metaData={metaData}
          onPageChange={(page: number) => dispatch(setPrParams({pageNumber: page}))}
        />
      </Grid>
    </Grid>
  );
}
