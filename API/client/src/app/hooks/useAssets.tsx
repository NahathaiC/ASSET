import { useEffect } from "react";
import {
  assetSelectors,
  fetchAssetsAsync,
} from "../../features/ASSET-catalog/AssetSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function useAssets() {
  const assets = useAppSelector(assetSelectors.selectAll);
  const { assetsLoaded, metaData } = useAppSelector(
    (state) => state.assetcatalog
  );
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (!assetsLoaded) dispatch(fetchAssetsAsync());
  }, [assetsLoaded, dispatch]);
 
  return {
    assets,
    assetsLoaded,
    metaData,
  };
}
