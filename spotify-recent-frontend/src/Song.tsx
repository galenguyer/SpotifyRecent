import React from 'react';
import './Song.css'

type SongProps = {
    name: string,
    artists: string[],
    id?: string
}

const Song: React.FunctionComponent<SongProps> = (props) => {
    const {name, artists, id} = props;
    var aggregatedArtists = "";
    if(artists.length === 1){
        aggregatedArtists = artists[0];
    }
    else {
        var i;
        for(i = 0; i < artists.length - 1; i++) {
            aggregatedArtists += artists[i] + ", ";
        }
        aggregatedArtists += artists[artists.length - 1];
    }
    return (
        <>
            <div className="song" id={id}>
                <span className="title">{name}</span> <span className="artists">- {aggregatedArtists}</span>
                <hr />
            </div>
            <br style={{clear: 'both'}} />
        </>
    );
}

export default Song;