import { AppBar, Box, List, ListItem, Switch, Toolbar, Typography, } from "@mui/material";
import { NavLink } from "react-router-dom";

 const midLinks = [
   //   { title: "catalog", path: "/catalog" },
   { title: "Error", path: "/about" },
   //   // {title: 'contact', path: '/contact'}
 ];

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
  return (
    <AppBar position="static" sx={{ mb: 4 }}>
      <Toolbar
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <Box display='flex' alignItems='center'>
          <Typography variant="h6" component={NavLink} to="/" sx={navStyles}>
            {" "}
            Purchase Requisition
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
          {rightLinks.map(({ title, path }) => (
            <ListItem component={NavLink} to={path} key={path} sx={navStyles}>
              {title.toUpperCase()}
            </ListItem>
          ))}
        </List>
      </Toolbar>
    </AppBar>
  );
}
