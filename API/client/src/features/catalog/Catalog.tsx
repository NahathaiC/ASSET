import LoadingComponent from "../../app/layout/LoadingComponent";
import PRList from "./PRList";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { setPageNumber, setPrParams } from "./catalogSlice";
import { FormLabel, Grid, Paper } from "@mui/material";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import CheckboxButtons from "../../app/components/CheckboxButtons";
import AppPagination from "../../app/components/AppPagination";
import usePRs from "../../app/hooks/usePRs";

const sortOptions = [
  { value: "CreatedDate", label: "วันที่ขอซื้อ" },
  { value: "UsingDate", label: "วันที่ต้องการใช้" },
];

export default function Catalog() {
  const {purchaserequisitions, department, section, metaData} = usePRs();
  const { status, prParams } = useAppSelector(
    (state) => state.catalog
  );
  const dispatch = useAppDispatch();

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
      <Grid item xs={9} sx={{mb: 2}}>
        <AppPagination 
          metaData={metaData}
          onPageChange={(page: number) => dispatch(setPageNumber({pageNumber: page}))}
        />
      </Grid>
    </Grid>
  );
}
