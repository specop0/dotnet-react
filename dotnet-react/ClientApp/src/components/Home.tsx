import React from "react";
import { Link, Typography } from "@mui/material";

const Home: React.FC = () => {
  return (
    <>
      <Typography>
        Edit <code>src/App.tsx</code> and save to reload.
      </Typography>
      <Link href="https://reactjs.org" target="_blank" rel="noopener noreferrer">
        Learn React
      </Link>
    </>
  );
};

export default Home;
