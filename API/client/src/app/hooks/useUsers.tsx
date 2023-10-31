import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import {
  fetchUsersAsync,
  userSelectors,
} from "../../features/account/userSlice";

//useUser.tsx
export function useUser() {
  const getusers = useAppSelector(userSelectors.selectAll);
  const { usersLoaded, metaData } = useAppSelector((state) => state.usercatalog);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (!usersLoaded) dispatch(fetchUsersAsync());
  }, [usersLoaded, dispatch]);

  return {
    getusers,
    usersLoaded,
    metaData,
  };
}
