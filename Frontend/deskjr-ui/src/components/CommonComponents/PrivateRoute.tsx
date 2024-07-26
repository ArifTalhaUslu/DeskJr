import { FC } from "react";
import { Route, Navigate, RouteProps, Routes } from "react-router-dom";

type PrivateRouteProps = RouteProps & {
  component: FC<any>;
};

const PrivateRoute: FC<PrivateRouteProps> = ({ component: Component, ...rest }) => {
  const token = localStorage.getItem("token");

  return (
    <Routes>
      <Route
        {...rest}
        element={token ? <Component /> : <Navigate to="/login" replace />}
      />
    </Routes>
  );
};

export default PrivateRoute;
