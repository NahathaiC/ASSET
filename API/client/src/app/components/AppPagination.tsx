import { Box, Pagination, Typography } from "@mui/material";
import { MetaData } from "../models/pagination";

interface Props {
  metaData: MetaData;
  onPageChange: (page: number) => void;
}

export default function AppPagination({ metaData, onPageChange }: Props) {
  const {currentPage, totalCount, totalPages, pageSize} = metaData;
  return (
    <Box display="flex" justifyContent="space-between" alignItems="center">
        <Typography>
            แสดงรายการที่ {(currentPage-1)*pageSize+1}-
            {currentPage*pageSize > totalCount 
                ? totalCount 
                : currentPage*pageSize} จากทั้งหมด {totalCount} รายการ
        </Typography>
      <Pagination
        color="primary"
        size="large"
        count={totalPages}
        page={currentPage}
        onChange={(e, page) => onPageChange(page)}
      />
    </Box>
  );
}
