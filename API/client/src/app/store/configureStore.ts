import { configureStore } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { catalogSlice } from "../../features/catalog/catalogSlice";
import { accountSlice } from "../../features/account/accountSlice";
import { assetSlice } from "../../features/ASSET-catalog/AssetSlice";
import { userSlice } from "../../features/account/userSlice";

export const store = configureStore({
    reducer: {
        assetcatalog: assetSlice.reducer,
        catalog: catalogSlice.reducer,
        account: accountSlice.reducer,
        usercatalog: userSlice.reducer,
    }
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;