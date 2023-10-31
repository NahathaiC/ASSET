import { Grid } from "@mui/material";
import { User } from "../../app/models/user";
import { useAppSelector } from "../../app/store/configureStore";

interface Props {
    users: User[];
}

export default function UserList({users} : Props) {
    const { usersLoaded } = useAppSelector((state) => state.usercatalog);
    return (
        <Grid container spacing={3}>
      {users.map((user) => (
        <Grid item xs={4} key={user.id}>
          {/* {!usersLoaded ? (
            <PRCardSkeleton />
          ) : (
            <PRCard users={users} />
          )} */}
        </Grid>
      ))}
    </Grid>
    )
}