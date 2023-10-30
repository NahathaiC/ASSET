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
import PendingActionsIcon from '@mui/icons-material/PendingActions';

// const midLinks = [{ title: "รายการขอซื้อ", path: "/pr-catalog" }];

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
        <Typography variant="h6">ระบบจัดการการจัดซื้อทรัพย์สิน</Typography>
        <Switch checked={darkMode} onChange={handleThemeChange} />
        {/* <List sx={{ display: "flex" }}>
          {midLinks.map(({ title, path }) => (
            <ListItem
              component={NavLink}
              to={path}
              key={path}
              sx={{ ...navStyles, fontSize: "0.9rem" }}
            >
              {title.toUpperCase()}
            </ListItem>
          ))}
        </List> */}
        {/* <List sx={{ display: "flex" }}>
          {user && user.roles?.includes("Admin") && (
            <ListItem
              component={NavLink}
              to={"/prstatus"}
              sx={{ ...navStyles, fontSize: "0.9rem" }}
            >
              จัดการสถานะการขอซื้อ
            </ListItem>
          )}
        </List> */}
        <List sx={{ display: "flex" }}>
          {user && (
            <ListItem
              component={NavLink}
              to={"/pr-catalog"}
              sx={{ ...navStyles, fontSize: "0.9rem" }}
            >
              รายการขอซื้อ
            </ListItem>
          )}
        </List>
        <List sx={{ display: "flex" }}>
          {user && (
            <ListItem
              component={NavLink}
              to={"/prform"}
              sx={{ ...navStyles, fontSize: "0.9rem" }}
            >
              สร้างใบขอจัดซื้อ
            </ListItem>
          )}
        </List>

        <List sx={{ display: "flex" }}>
          {user && (
            <ListItem
              component={NavLink}
              to={"/asset-catalog"}
              sx={{ ...navStyles, fontSize: "0.9rem" }}
            >
              รายการทรัพย์สิน
            </ListItem>
          )}
        </List>

        <List sx={{ display: "flex" }}>
          {user && (
            <ListItem
              component={NavLink}
              to={"/pr-report"}
              sx={{ ...navStyles, fontSize: "0.9rem" }}
            >
              รายงานการขอซื้อ
            </ListItem>
          )}
        </List>

        {user ? (
          <SignedInMenu />
        ) : (
          <List sx={{ display: "flex" }}>
            {rightLinks.map(({ title, path }) => (
              <ListItem
                component={NavLink}
                to={path}
                key={path}
                sx={{ ...navStyles, fontSize: "0.9rem" }}
              >
                {title.toUpperCase()}
              </ListItem>
            ))}
          </List>
        )}
        <Box display="flex" alignItems="center">
          <Typography
            variant="h6"
            component={NavLink}
            to="/prstatus"
            sx={{ ...navStyles, fontSize: "0.9rem", alignItems: "center" }}
          >
            {user && user.roles?.includes("Approver") && (
              <PendingActionsIcon sx={{ fontSize: 25 }} />
            )}
          </Typography>
        </Box>
      </Toolbar>
    </AppBar>
  );
}
