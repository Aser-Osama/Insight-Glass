import React, { useState, useEffect, createContext } from "react";
import { Navigate } from "react-router-dom";
import axios from "axios";

const UserContext = createContext({});

function AuthorizeView({
  children,
  showLoadingMsg,
  redirectToLoginPage,
  onIsLoggedIn,
}) {
  const [authorized, setAuthorized] = useState(false);
  const [loading, setLoading] = useState(true);
  let emptyUser = { email: "" };

  const [user, setUser] = useState(emptyUser);

  useEffect(() => {
    let retryCount = 0;
    let maxRetries = 5;
    let delay = 1000;

    async function fetchWithRetry(url, options) {
      try {
        let response = await axios.get(url, options);

        if (response.status === 200) {
          console.log("Authorized");
          let j = response.data;
          setUser({ email: j.email });
          setAuthorized(true);
          onIsLoggedIn(true);
          return response;
        } else if (response.status === 401) {
          console.log("Unauthorized");
          onIsLoggedIn(false);
          return response;
        } else {
          throw new Error("" + response.status);
        }
      } catch (error) {
        retryCount++;
        if (retryCount > maxRetries) {
          throw error;
        } else {
          await new Promise((resolve) => setTimeout(resolve, delay));
          return fetchWithRetry(url, options);
        }
      }
    }

    fetchWithRetry("/pingauth", {})
      .catch((error) => {
        console.log(error.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <>{showLoadingMsg && <p>Loading...</p>}</>;
  } else {
    if (authorized && !loading) {
      return (
        <>
          <UserContext.Provider value={user}>{children}</UserContext.Provider>
        </>
      );
    } else {
      return <>{redirectToLoginPage && <Navigate to="/signin" />}</>;
    }
  }
}

export function AuthorizedUser({ value }) {
  // Consume the username from the UserContext
  const user = React.useContext(UserContext);
  // Display the username in a h1 tag
  if (value == "email") return <>{user.email}</>;
  else return <></>;
}

export default AuthorizeView;
