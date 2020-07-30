import React from 'react';
import Parkinfo from './parkinfo';
import Footer from '../Share/footer';

import Loop1 from '../../media/Carousel/loop_1.jpg';

function Park() {
  return (
    <div className="parkpage">
      <div className="map-info">
        <img src={Loop1} alt="map" />
      </div>
      <Parkinfo />
      <Footer />
    </div>
  );
}

export default Park;
