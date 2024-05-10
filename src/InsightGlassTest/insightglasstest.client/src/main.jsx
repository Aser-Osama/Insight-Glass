import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import HomePage from "./pages/Home/Home.jsx";
import SearchResultForCompany from "./pages/Home/SearchResultforCompany.jsx";
import SearchResultForJob from "./pages/Home/SearchResultforjob.jsx";
import SignUpPage from "./pages/AuthPages/Register.jsx";
import SignIn from "./pages/AuthPages/Signin.jsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "/",
        element: <HomePage />,
      },
      {
        path: "/search_company",
        element: <SearchResultForCompany />,
      },
      {
        path: "/search_job",
        element: <SearchResultForJob />,
      },
      {
        path: "/signin",
        element: <SignIn />,
      },
      {
        path: "/register",
        element: <SignUpPage />,
      },
      {
        path: "*",
        element: <p> Page not found... </p>,
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
