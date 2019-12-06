export interface Track {
    name: string,
    artists: string[],
}

export interface History {
    currentTrack?: Track,
    tracks: Track[],
}