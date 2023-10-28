import { useState } from "react";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { setAssetParams } from "./AssetSlice";
import { TextField, debounce } from "@mui/material";

export default function AssetSearch() {
  const { assetParams } = useAppSelector((state) => state.assetcatalog);
  const [searchTerm, setSearchTerm] = useState(assetParams.searchTerm);
  const dispatch = useAppDispatch();

  const debouncedSearch = debounce((event: any) => {
    dispatch(setAssetParams({ searchTerm: event?.target.value }))
  }, 1000)

  return (
    <TextField
      label="ค้นหาด้วยชื่อ"
      variant="outlined"
      fullWidth
      value={searchTerm || ''}
      onChange={(event: any) => {
        setSearchTerm(event.target.value);
        debouncedSearch(event);
    }}
    />
  );
}
