import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import HomePage from "./pages/Home/Home.jsx";
import SearchResultForCompany from "./pages/Home/SearchResultforCompany.jsx";
import SearchResultForJob from "./pages/Home/SearchResultforjob.jsx";
import SignUpPage from "./pages/AuthPages/Register.jsx";
import SignIn from "./pages/AuthPages/Signin.jsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import CompanyProfile from "./pages/Company/Company.jsx";
import AdminPage from "./pages/Admin/Admin.jsx";
import UserProfile from "./pages/Profiles/UserProfile.jsx";
import AuthorizeView from "./components/AuthorizeView.jsx";
import React from "react";
function placeholder(intval) {
  return;
}

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
        path: "/userprofile",
        element: (
          <AuthorizeView
            showLoadingMsg={true}
            redirectToLoginPage={true}
            onIsLoggedIn={placeholder}
          >
            <UserProfile />{" "}
          </AuthorizeView>
        ),
      },
      {
        path: "/companyprofile",
        element: (
          <AuthorizeView
            showLoadingMsg={true}
            redirectToLoginPage={true}
            onIsLoggedIn={placeholder}
          >
            <CompanyProfile />
          </AuthorizeView>
        ),
      },
      {
        path: "/adminprofile",
        element: (
          <AuthorizeView
            showLoadingMsg={true}
            redirectToLoginPage={true}
            onIsLoggedIn={placeholder}
          >
            <AdminPage />{" "}
          </AuthorizeView>
        ),
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
