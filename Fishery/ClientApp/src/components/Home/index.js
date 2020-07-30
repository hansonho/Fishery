import React from 'react';
import { Link } from 'react-router-dom';
import Carousel from './carousel';
import Marquee from './marquee';
import Btnlink from '../Share/btnlink';

import activity from '../../media/HomeBtnImg/activity.png';
import broad from '../../media/HomeBtnImg/broad.png';
import entertainment from '../../media/HomeBtnImg/entertainment.png';
import food from '../../media/HomeBtnImg/food.png';
import photography from '../../media/HomeBtnImg/photography.png';
import sights from '../../media/HomeBtnImg/sights.png';
import Loop1 from '../../media/Carousel/loop_1.jpg';
// import Loop2 from '../../media/Carousel/loop_2.jpg';
import parking from '../../media/HomeBtnImg/parking.png';
// import video from '../../media/Carousel/h264.480.60s.mp4';

function Home() {
    const picData = [
        {
            index: 0,
            src: sights,
            alt: '景點介紹',
            href: '/scenic.html',
        },
        {
            index: 1,
            src: food,
            alt: '美食介紹',
            href: '/food.html',
        },
        {
            index: 2,
            src: entertainment,
            alt: '娛樂漁船',
            href: '/boat.html',
        },
        {
            index: 3,
            src: activity,
            alt: '活動訊息',
            href: '/activity',
        },
        {
            index: 4,
            src: broad,
            alt: '船流直播',
            href: '',
        },
        {
            index: 5,
            src: photography,
            alt: '水下攝影',
            href: '/photography',
        },
    ];
    const carouselData = [
        {
            index: 0,
            type: 'pic',
            src: Loop1,
            alt: 'Loop1'
        },
        /*{
            index: 1,
            type: 'pic',
            src: Loop2,
            alt: 'Loop2'
        },
        {
            index: 2,
            type: 'video',
            src: video,
        }*/
    ];
    if (window.screen.width === 1080) {
        return (
            <div className="homepage">
                <Carousel carouselData={carouselData} />
                <Marquee />
                <Btnlink className="homelink-info" picData={picData} />
            </div>
        );
    }
    return (
        <div className="homepage">
            <Carousel carouselData={carouselData} />
            <Marquee />
            <Btnlink className="homelink-info" picData={picData} />
            <div className="park-btn">
                <Link to="/park">
                    <img src={parking} alt="parking" />
                </Link>
            </div>
        </div>
    );
}

export default Home;