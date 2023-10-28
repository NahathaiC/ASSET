import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice,
} from "@reduxjs/toolkit";
import { Asset, AssetParams } from "../../app/models/asset";
import agent from "../../app/api/agent";
import { RootState } from "../../app/store/configureStore";
import { MetaData } from "../../app/models/pagination";
import AssetCatalog from "./AssetCatalog";

interface AssetCatalogState {
  assetsLoaded: boolean;
  status: string;
  assetParams: AssetParams;
  metaData: MetaData | null;
}

const assetsAdapter = createEntityAdapter<Asset>();

function getAxiosParams(assetParams: AssetParams) {
  const params = new URLSearchParams();
  params.append("pageNumber", assetParams.pageNumber.toString());
  params.append("pageSize", assetParams.pageSize.toString());
  params.append("orderBy", assetParams.orderBy);

  if (assetParams.searchTerm) params.append("searchTerm", assetParams.searchTerm);
  return params;

}

export const fetchAssetsAsync = createAsyncThunk<
  Asset[],
  void,
  {state: RootState}>
  ("assetcatalog/fetchAssetsAsync", async (_, thunkAPI) => {
    const params = getAxiosParams(thunkAPI.getState().assetcatalog.assetParams);
    try {
      const response = await agent.AssetCatalog.list(params);
      thunkAPI.dispatch(setMetaData(response.metaData));
      return response.items;
    } catch (error: any) {
      return thunkAPI.rejectWithValue({error: error.data});
    }
  });

export const fetchAssetAsync = createAsyncThunk<Asset, string>(
  "assetcatalog/fetchAssetAsync",
  async (assetId) => {
    try {
      return await agent.AssetCatalog.details(assetId);
    } catch (error: any) {
      console.log(error);
    }
  }
);

function initParams() {
  return {
    pageNumber: 1,
    pageSize: 6,
    orderBy: "id",
    department: [],
    section: [],
  };
}

export const assetSlice = createSlice({
  name: "assetcatalog",
  initialState: assetsAdapter.getInitialState<AssetCatalogState>({
    ...assetsAdapter.getInitialState(),
    assetsLoaded: false,
    status: "idle",
    assetParams: initParams(),
    metaData: null,
  }),
  reducers: {
    setAssetParams: (state, action) => {
      state.assetsLoaded = false;
      state.assetParams = { ...state.assetParams, ...action.payload, pageNumber: 1 };
    },
    setPageNumber: (state, action) => {
      state.assetsLoaded = false;
      state.assetParams = {...state.assetParams, ...action.payload};
    },
    setMetaData: (state, action) => {
      state.metaData = action.payload;
    },
    resetAssetParams: (state) => {
      state.assetParams = initParams();
    }
  },
  extraReducers: (builder => {
    builder.addCase(fetchAssetsAsync.pending, (state) => {
      state.status = "pendingFetchAssets";
    });
    builder.addCase(fetchAssetsAsync.fulfilled, (state, action) => {
      assetsAdapter.setAll(state, action.payload);
      state.status = "idle";
      state.assetsLoaded = true;
    });
    builder.addCase(fetchAssetsAsync.rejected, (state) => {
      state.status = "idle";
    });
    builder.addCase(fetchAssetAsync.pending, (state) => {
        state.status = 'pendingFetchAsset';
    });
    builder.addCase(fetchAssetAsync.fulfilled, (state, action) => {
        assetsAdapter.upsertOne(state, action.payload);
        state.status = 'idle';
    });
    builder.addCase(fetchAssetAsync.rejected, (state) => {
        state.status = 'idle';
    })

  })
})

export const assetSelectors = assetsAdapter.getSelectors(
  (state: RootState) => state.assetcatalog
);

export const { setAssetParams, resetAssetParams, setMetaData, setPageNumber } = assetSlice.actions;

export default assetSlice.reducer;
