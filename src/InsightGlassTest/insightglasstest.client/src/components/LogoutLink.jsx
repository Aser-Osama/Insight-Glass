function LogoutLink({ children }) {
  const handleSubmit = (e) => {
    e.preventDefault();
    fetch("/logout", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: "",
    })
      .then((data) => {
        if (data.ok) {
          window.location.href = "/";
        } else {
          console.error("Error logging out");
        }
      })
      .catch((error) => {
        console.error(error);
      });
  };

  return (
    <>
      <a href="#" onClick={handleSubmit}>
        {children}
      </a>
    </>
  );
}

export default LogoutLink;
