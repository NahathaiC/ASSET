import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice,
} from "@reduxjs/toolkit";
import { MetaData } from "../../app/models/pagination";
import { UserDto, UserParams } from "../../app/models/user";
import { RootState } from "../../app/store/configureStore";
import agent from "../../app/api/agent";

interface UserState {
  usersLoaded: boolean;
  status: string;
  userParams: UserParams;
  metaData: MetaData | null;
}

const usersAdapter = createEntityAdapter<UserDto>();

function getAxiosParams(userParams: UserParams) {
  const params = new URLSearchParams();
  params.append("pageNumber", userParams.pageNumber.toString());
  params.append("pageSize", userParams.pageSize.toString());

  return params;
}

export const fetchUsersAsync = createAsyncThunk<
  UserDto[],
  void,
  { state: RootState }
>("usercatalog/fetchUsersAsync", async (_, thunkAPI) => {
  const params = getAxiosParams(thunkAPI.getState().usercatalog.userParams);
  try {
    const response = await agent.User.list(params);
    console.log("API Response Data:", response);

    thunkAPI.dispatch(setMetaData(response.metaData));
    return response;
  } catch (error) {
    console.error("Error fetching users:", error);
    return thunkAPI.rejectWithValue({ error: error });
  }
});

function initParams() {
  return {
    pageNumber: 1,
    pageSize: 6,
  };
}

export const userSlice = createSlice({
  name: "usercatalog",
  initialState: usersAdapter.getInitialState<UserState>({
    ...usersAdapter.getInitialState(),
    usersLoaded: false,
    status: "idle",
    userParams: initParams(),
    metaData: null,
  }),
  reducers: {
    setUserParams: (state, action) => {
      state.usersLoaded = false;
      state.userParams = {
        ...state.userParams,
        ...action.payload,
        pageNumber: 1,
      };
    },
    setPageNumber: (state, action) => {
      state.usersLoaded = false;
      state.userParams = { ...state.userParams, ...action.payload };
    },
    setMetaData: (state, action) => {
      state.metaData = action.payload;
    },
    resetUserParams: (state) => {
      state.userParams = initParams();
    },
    setUser: (state, action) => {
      usersAdapter.upsertOne(state, action.payload);
      state.usersLoaded = false;
    }
  },
  extraReducers: (builder) => {
    builder.addCase(fetchUsersAsync.pending, (state) => {
      state.status = "pendingFetchUsers";
    });

    builder.addCase(fetchUsersAsync.fulfilled, (state, action) => {
      usersAdapter.setAll(state, action.payload);
      state.status = "idle";
      state.usersLoaded = true;
    });

    builder.addCase(fetchUsersAsync.rejected, (state) => {
      state.status = "idle";
    });
  },
});

export const userSelectors = usersAdapter.getSelectors(
  (state: RootState) => state.usercatalog
);

export const { setUserParams, resetUserParams, setMetaData, setPageNumber, setUser } =
  userSlice.actions;
export default userSlice.reducer;
