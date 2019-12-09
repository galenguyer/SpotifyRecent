export interface Track {
    name: string,
    artists: string[],
    isPlaying: boolean,
}

export interface History {
    currentTrack?: Track,
    tracks: Track[],
}