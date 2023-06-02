import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice,
} from "@reduxjs/toolkit";
import {
  PurchaseRequisition,
  prParams,
} from "../../app/models/purchaseRequisition";
import agent from "../../app/api/agent";
import { RootState } from "../../app/store/configureStore";
import { MetaData } from "../../app/models/pagination";

interface CatalogState {
  prsLoaded: boolean;
  filtersLoaded: boolean;
  status: string;
  department: string[];
  section: string[];
  prParams: prParams;
  metaData: MetaData | null;
}

const prAdapter = createEntityAdapter<PurchaseRequisition>();

function getAxiosParams(prParams: prParams) {
  const params = new URLSearchParams();
  params.append("pageNumber", prParams.pageNumber.toString());
  params.append("pageSize", prParams.pageSize.toString());
  params.append("orderBy", prParams.orderBy);

  if (prParams.seachTem) params.append("searchTerm", prParams.seachTem);
  if (prParams.department.length > 0)
    params.append("department", prParams.department.toString());
  if (prParams.section.length > 0)
    params.append("section", prParams.section.toString());
  return params;
}

export const fetchPRsAsync = createAsyncThunk<
  PurchaseRequisition[],
  void,
  { state: RootState }
>("catalog/fetchPRsAsync", async (_, thunkAPI) => {
  const params = getAxiosParams(thunkAPI.getState().catalog.prParams);
  try {
    const response = await agent.Catalog.list(params);
    thunkAPI.dispatch(setMetaData(response.metaData));
    return response.items;
  } catch (error: any) {
    return thunkAPI.rejectWithValue({ error: error.data });
  }
});

export const fetchPRAsync = createAsyncThunk<PurchaseRequisition, number>(
  "catalog/fetchPRAsync",
  async (PRId) => {
    try {
      const purchaseRequisition = await agent.Catalog.details(PRId);
      return purchaseRequisition;
    } catch (error) {
      console.log(error);
      throw error; // Rethrow the error to be caught by the rejection handler
    }
  }
);

export const fetchFilters = createAsyncThunk(
  "catalog/fetchFilters",
  async (_, thunkAPI) => {
    try {
      return agent.Catalog.fetchFilter();
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ eror: error.data });
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

export const catalogSlice = createSlice({
  name: "catalog",
  initialState: prAdapter.getInitialState<CatalogState>({
    ...prAdapter.getInitialState(),
    prsLoaded: false,
    filtersLoaded: false,
    status: "idle",
    department: [],
    section: [],
    prParams: initParams(),
    metaData: null,
  }),
  reducers: {
    setPrParams: (state, action) => {
      state.prsLoaded = false;
      state.prParams = { ...state.prParams, ...action.payload, pageNumber: 1 };
    },
    setPageNumber: (state, action) => {
      state.prsLoaded = false;
      state.prParams = { ...state.prParams, ...action.payload };
    },
    setMetaData: (state, action) => {
      state.metaData = action.payload;
    },
    resetPrParams: (state) => {
      state.prParams = initParams();
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchPRsAsync.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(fetchPRsAsync.fulfilled, (state, action) => {
      prAdapter.setAll(state, action.payload);
      state.status = "idle";
      state.prsLoaded = true;
    });
    builder.addCase(fetchPRsAsync.rejected, (state) => {
      state.status = "error";
    });

    //PR
    builder.addCase(fetchPRAsync.pending, (state) => {
      state.status = "pendingFetchPurchaseRequisitions";
    });
    builder.addCase(fetchPRAsync.fulfilled, (state, action) => {
      prAdapter.upsertOne(state, action.payload);
      state.status = "idle";
    });
    builder.addCase(fetchPRAsync.rejected, (state) => {
      state.status = "idle";
    });

    //Filter
    builder.addCase(fetchFilters.pending, (state) => {
      state.status = "pendingFetchFiltes";
    });
    builder.addCase(fetchFilters.fulfilled, (state, action) => {
      state.department = action.payload.department;
      state.section = action.payload.section;
      state.filtersLoaded = true;
    });
    builder.addCase(fetchFilters.rejected, (state, action) => {
      state.status = "idle";
      console.log(action.payload);
    });
  },
});

export const prSelectors = prAdapter.getSelectors(
  (state: RootState) => state.catalog
);

export const { setPrParams, resetPrParams, setMetaData, setPageNumber } =
  catalogSlice.actions;

export default catalogSlice.reducer;
