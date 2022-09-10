import React from "react";
import { useLocation } from "react-router-dom";

const NotFound: React.FC = () => {
  const location = useLocation();
  const path = location.pathname ?? "/";
  return <><code>{path}</code> not found</>;
};

export default NotFound;
