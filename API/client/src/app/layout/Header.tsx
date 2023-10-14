import {
  AppBar,
  Box,
  List,
  ListItem,
  Switch,
  Toolbar,
  Typography,
} from "@mui/material";
import { NavLink } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";
import SignedInMenu from "./SignedInMenu";

const midLinks = [{ title: "รายการขอซื้อ", path: "/catalog" }];

const rightLinks = [
  { title: "login", path: "/login" },
  { title: "register", path: "/register" },
];

const navStyles = {
  color: "inherit",
  textDecoration: "none",
  typography: "h6",
  "&:hover": {
    color: "grey.500",
  },
  "&.active": {
    color: "text.secondary",
  },
};

interface Props {
  darkMode: boolean;
  handleThemeChange: () => void;
}

export default function Header({ darkMode, handleThemeChange }: Props) {
  const { user } = useAppSelector((state) => state.account);

  return (
    <AppBar position="static" sx={{ mb: 6 }}>
      <Toolbar
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <Box display="flex" alignItems="center">
          <Typography variant="h6" component={NavLink} to="/" sx={navStyles}>
            {" "}
            HomePage
          </Typography>
          <Switch checked={darkMode} onChange={handleThemeChange} />
        </Box>

        <List sx={{ display: "flex" }}>
          {midLinks.map(({ title, path }) => (
            <ListItem component={NavLink} to={path} key={path} sx={navStyles}>
              {title.toUpperCase()}
            </ListItem>
          ))}
        </List>
        <List sx={{ display: "flex" }}>
          {user && user.roles?.includes("Admin") && (
            <ListItem component={NavLink} to={"/inventory"} sx={navStyles}>
              จัดการสถานะการขอซื้อ
            </ListItem>
          )}
        </List>
        <List sx={{ display: "flex" }}>
          {user && (
            <ListItem component={NavLink} to={"/prform"} sx={navStyles}>
              สร้างใบข้อซื้อ
            </ListItem>
          )}
        </List>

        {user ? (
          <SignedInMenu />
        ) : (
          <List sx={{ display: "flex" }}>
            {rightLinks.map(({ title, path }) => (
              <ListItem component={NavLink} to={path} key={path} sx={navStyles}>
                {title.toUpperCase()}
              </ListItem>
            ))}
          </List>
        )}
      </Toolbar>
    </AppBar>
  );
}
