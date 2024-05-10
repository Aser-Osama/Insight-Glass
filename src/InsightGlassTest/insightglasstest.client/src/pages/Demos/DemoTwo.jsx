import { useEffect } from "react";
import { useParams, useSearchParams } from "react-router-dom";

function DemoTwo() {
  const { userId } = useParams();
  const [searchParams] = useSearchParams();
  useEffect(() => {
    const currentParams = Object.fromEntries([...searchParams]);
    console.log(currentParams); // get new values onchange
  }, [searchParams]);

  const paramObj = Object.fromEntries([...searchParams]);

  return (
    <div>
      This is a second demo page
      <p>{userId && `This is ur url param: ${userId}`}</p>
      <p>
        {paramObj.name &&
          `this is a url query: ${paramObj.q} and ${paramObj.name}`}
      </p>
    </div>
  );
}

export default DemoTwo;
