import React, { useState, useEffect  } from 'react';
import Slider from "react-slick";

import '../../css/carousel.css';

function Carousel() {
    const [autopPlaySpeed, setAutoPlaySpeed] = useState(5000);
    const [carouselData, setCarouselData] = useState([]);
    const settings = {
        dots: false,
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        autoplay: true,
        speed: 1000,
        autoplaySpeed: autopPlaySpeed,
        afterChange: (e) => {
            if (document.getElementsByClassName('slick-active')[0].getElementsByClassName('item')[0].childNodes[0].tagName.toLowerCase() === 'video') {
                document.getElementsByClassName('slick-active')[0].getElementsByClassName('item')[0].childNodes[0].play();
                const durationTime = document.getElementsByClassName('slick-active')[0].getElementsByClassName('item')[0].childNodes[0].duration * 1000;
                const currentTime = document.getElementsByClassName('slick-active')[0].getElementsByClassName('item')[0].childNodes[0].currentTime * 1000;
                setAutoPlaySpeed(durationTime - currentTime);
            } else {
                setAutoPlaySpeed(5000);
            }
        },
    };

    useEffect(() => {
        fetch('api/Carousels')
            .then(response => {
                return response.json();
            })
            .then(json => {
                setCarouselData(json);
            })
            .catch(err => {
                console.error(err);
            });
    }, []);

    //let carouselData = [{ "id": 1, "type": "pic", "src": "loop_1.jpg", "alt": "Loop1" }, { "id": 2, "type": "pic", "src": "exercise-taipei.png", "alt": "110年全國運動會在新北" }];


    return (
        <div className="carousel-info">
            <Slider {...settings}>
                {carouselData.map((e) => {
                    if (e.type === 'pic') {
                        return (
                            <div key={e.id} className="item">
                                <img src={"/images/carousel/"+e.src} alt={e.alt} />
                            </div>
                        );
                    }
                    return (
                        <div key={e.id} className="item">
                            {/* <iframe className="hiddenIframe" src={e.src} allow="autoplay" title="hiddenVideo"></iframe> */}
                            <video autoPlay muted>
                                <source src={"/images/carousel/" +e.src} type="video/mp4" />
                            </video>
                        </div>
                    );
                })}
            </Slider>
        </div>
    );
}

export default Carousel;
