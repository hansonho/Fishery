import React, { useState, useEffect } from 'react';

function Parkinfo() {
  const [park, setPark] = useState([]);
  /*useEffect(() => {
    async function fetchData() {
      const response = await fetch(' /api/parkingspace', {
        method: 'GET',
        headers: new Headers({
          'Content-Type': 'application/json',
        }),
      });
      const json = await response.json();
      const data = JSON.parse(json);
      setPark([...park, data.Data]);
    }
    fetchData();
  });*/
  let parkinfo = {
    "Data": [
      {
        "Status": "0",
        "StatusDesc": "Success",
        "ParkingName": "深澳漁港第一場",
        "Time": "20200730172259",
        "ParkingSpace": 133
      },
      {
        "Status": "0",
        "StatusDesc": "Success",
        "ParkingName": "深澳漁港第二場",
        "Time": "20200730172259",
        "ParkingSpace": 133
      },
      {
        "Status": "0",
        "StatusDesc": "Success",
        "ParkingName": "深澳漁港第三場",
        "Time": "20200730172259",
        "ParkingSpace": 1
      }
    ]
  }
  return (
    <div className="park-info">
      {parkinfo.Data.map((e, i) => {
        /* let parkDetail = 'park-detail';
        if (e.ParkingSpace < 10) {
          parkDetail += ' nopark'
        } */
        if (e.Status === "0") {
          return (
            <div className="item" key={i}>
              <div className="park-name">
                <p>{e.ParkingName}</p>
              </div>
              <div className="park-detail">
                <span>{`目前尚有車位${e.ParkingSpace}個`}</span>
              </div>
            </div>
          );
        } else {
          console.log(e)
          return null;
        }
      })}
    </div>
  );
}

export default Parkinfo;