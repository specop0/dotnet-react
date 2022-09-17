import React from "react";
import { AppBar, Box, Button, Toolbar } from "@mui/material";
import { Link, Outlet } from "react-router-dom";

const Layout = () => {
  const pages = [
    {
      name: "Home",
      url: "/",
    },
    {
      name: "Weather",
      url: "/weather",
    },
  ];

  return (
    <React.Fragment>
      <AppBar component="nav" position="fixed">
        <Toolbar>
          {pages.map((page) => (
            <Button to={page.url} color="inherit" component={Link}>
              {page.name}
            </Button>
          ))}
        </Toolbar>
      </AppBar>
      <Box component="main">
        <Toolbar />
        <Outlet />
      </Box>
    </React.Fragment>
  );
};

export default Layout;
