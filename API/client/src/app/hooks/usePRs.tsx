import { useEffect } from "react";
import { prSelectors, fetchPRsAsync, fetchFilters } from "../../features/catalog/catalogSlice";
import { useAppSelector, useAppDispatch } from "../store/configureStore";

export default function usePRs() {
  const purchaserequisitions = useAppSelector(prSelectors.selectAll);
  const {
    prsLoaded,
    filtersLoaded,
    department,
    section,
    metaData,
  } = useAppSelector((state) => state.catalog);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (!prsLoaded) dispatch(fetchPRsAsync());
  }, [prsLoaded, dispatch]);

  useEffect(() => {
    if (!filtersLoaded) dispatch(fetchFilters());
  }, [dispatch, filtersLoaded]);

  return {
    purchaserequisitions,
    prsLoaded,
    filtersLoaded,
    department,
    section,
    metaData
  }
}
